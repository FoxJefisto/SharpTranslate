using SharpTranslate.Models;
using SharpTranslate.Models.DatabaseModels;
using SharpTranslate.Repositories;
using SharpTranslate.Repositories.Interfaces;
using SharpTranslate.Services.Interfaces;

namespace SharpTranslate.Services
{
    public class UserWordsManager : IUserWordsManager
    {
        IUserRepository _userRepository;
        IUserWordPairRepository _userWordPairRepository;
        IWordPairRepository _wordPairRepository;
        IWordRepository _wordRepository;

        public UserWordsManager( 
            IUserRepository userRepository,
            IUserWordPairRepository userWordPairRepository,
            IWordPairRepository wordPairRepository,
            IWordRepository wordRepository)
        {
            _userRepository = userRepository;
            _userWordPairRepository = userWordPairRepository;
            _wordPairRepository = wordPairRepository;
            _wordRepository = wordRepository;
        }

        public int? AddWord(WordRequestBody? body)
        {
            if (body == null || body.Word == null) 
                throw new ArgumentNullException("Неверное тело запроса");

            if (body.TargetWord == null)
                throw new ArgumentNullException("Не найден перевод слова");

            var user = _userRepository.GetUserById(body.UserId);
            if (user == null)
                throw new ArgumentException("Пользователь не найден");

            var wordOriginal = _wordRepository.GetWordByWordName(body.Word);
            if (wordOriginal == null)
            {
                wordOriginal = new Word
                {
                    Language = body.SourceLanguage,
                    WordName = body.Word
                };
                _wordRepository.AddWord(wordOriginal);
            }

            var wordTranslation = _wordRepository.GetWordByWordName(body.TargetWord);
            if (wordTranslation == null)
            {
                wordTranslation = new Word
                {
                    Language = body.TargetLanguage,
                    WordName = body.TargetWord
                };
                _wordRepository.AddWord(wordTranslation);
            }

            var wordPair = _wordPairRepository.GetWordPairByWords(wordOriginal, wordTranslation);
            if(wordPair == null)
            {
                wordPair = new WordPair
                {
                    OriginalWord = wordOriginal,
                    TranslationWord = wordTranslation
                };
                _wordPairRepository.AddWordPair(wordPair);
            }

            var userWordPair = _userWordPairRepository.GetUserWordPairByWordPair(user, wordPair);
            if (userWordPair != null)
                throw new Exception("Данное слово уже привязано к текущему пользователю");

            userWordPair = new UserWordPair
            {
                User = user,
                WordPair = wordPair,
                WordStatus = body.UserWordStatus
            };

            _userWordPairRepository.AddUserWordPair(userWordPair);

            return userWordPair.Id;
        }

        public void DeleteWord(int userWordPairId)
        {
            var userWordPair = _userWordPairRepository.GetUserWordPairById(userWordPairId);

            if (userWordPair == null)
                throw new Exception("Не удалось найти данное слово в словаре пользователя");

            var wordPair = userWordPair.WordPair;

            _userWordPairRepository.DeleteUserWordPair(userWordPair!);

            if(!_userWordPairRepository.GetAllUserWordPairs().Any(x => x.WordPairId == wordPair!.Id))
            {
                var originalWord = wordPair?.OriginalWord!;
                var translationWord = wordPair?.TranslationWord!;
                _wordPairRepository.DeleteWordPair(wordPair!);

                if(!_wordPairRepository.GetAllWordPairs()
                    .Any(x => x.OriginalWordId == originalWord.Id || x.TranslationWordId == originalWord.Id))
                {
                    _wordRepository.DeleteWord(originalWord);
                }

                if(!_wordPairRepository.GetAllWordPairs()
                    .Any(x => x.OriginalWordId == translationWord.Id || x.TranslationWordId == translationWord.Id))
                {
                    _wordRepository.DeleteWord(translationWord);
                }
            }
        }

        public List<UserWordPair?> GetAllUsersWordPairs()
        {
            return _userWordPairRepository.GetAllUserWordPairs()!;
        }

        public List<UserWordPair?> GetUserWordPairsByUserId(int userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
                throw new ArgumentException("Пользователь не найден");

            return user.UserWords!.ToList();
        }

        public UserWordPair? UpdateWord(int userWordPairId, WordRequestBody body)
        {
            var wordOriginal = _wordRepository.GetWordByWordName(body.Word);
            if (wordOriginal == null)
            {
                wordOriginal = new Word
                {
                    Language = body.SourceLanguage,
                    WordName = body.Word
                };
                _wordRepository.AddWord(wordOriginal);
            }

            var wordTranslation = _wordRepository.GetWordByWordName(body.TargetWord);
            if (wordTranslation == null)
            {
                wordTranslation = new Word
                {
                    Language = body.TargetLanguage,
                    WordName = body.TargetWord
                };
                _wordRepository.AddWord(wordTranslation);
            }

            var wordPair = _wordPairRepository.GetWordPairByWords(wordOriginal, wordTranslation);
            if (wordPair == null)
            {
                wordPair = new WordPair
                {
                    OriginalWord = wordOriginal,
                    TranslationWord = wordTranslation
                };
                _wordPairRepository.AddWordPair(wordPair);
            }

            var userWordPair = _userWordPairRepository.GetUserWordPairById(userWordPairId);
            if (userWordPair == null)
            {
                throw new ArgumentException("Не удалось найти слово в словаре пользователя");
            }
            userWordPair.WordPair = wordPair;
            userWordPair.WordStatus = body.UserWordStatus;

            _userWordPairRepository.UpdateUserWordPair(userWordPair);

            return userWordPair;
        }
    }
}
