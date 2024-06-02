import requests

url = "http://localhost:80/health"

for i in range(100):
    response = requests.get(url)
    print(response)