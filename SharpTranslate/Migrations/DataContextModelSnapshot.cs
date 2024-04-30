﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SharpTranslate.Models.DatabaseModels.Core;

#nullable disable

namespace SharpTranslate.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SharpTranslate.Models.DatabaseModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("SharpTranslate.Models.DatabaseModels.UserWordPair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("WordPairId")
                        .HasColumnType("integer");

                    b.Property<int>("WordStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WordPairId");

                    b.ToTable("user_wordpair");
                });

            modelBuilder.Entity("SharpTranslate.Models.DatabaseModels.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Language")
                        .HasColumnType("text");

                    b.Property<string>("WordName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("word");
                });

            modelBuilder.Entity("SharpTranslate.Models.DatabaseModels.WordPair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("OriginalWordId")
                        .HasColumnType("integer");

                    b.Property<int>("TranslationWordId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OriginalWordId");

                    b.HasIndex("TranslationWordId");

                    b.ToTable("wordpair");
                });

            modelBuilder.Entity("SharpTranslate.Models.DatabaseModels.UserWordPair", b =>
                {
                    b.HasOne("SharpTranslate.Models.DatabaseModels.User", "User")
                        .WithMany("UserWords")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SharpTranslate.Models.DatabaseModels.WordPair", "WordPair")
                        .WithMany()
                        .HasForeignKey("WordPairId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WordPair");
                });

            modelBuilder.Entity("SharpTranslate.Models.DatabaseModels.WordPair", b =>
                {
                    b.HasOne("SharpTranslate.Models.DatabaseModels.Word", "OriginalWord")
                        .WithMany()
                        .HasForeignKey("OriginalWordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SharpTranslate.Models.DatabaseModels.Word", "TranslationWord")
                        .WithMany()
                        .HasForeignKey("TranslationWordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OriginalWord");

                    b.Navigation("TranslationWord");
                });

            modelBuilder.Entity("SharpTranslate.Models.DatabaseModels.User", b =>
                {
                    b.Navigation("UserWords");
                });
#pragma warning restore 612, 618
        }
    }
}