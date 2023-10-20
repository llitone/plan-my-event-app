import requests

response = requests.post("http://192.168.0.48:4121/mail-confirmation/api/v1.0/send-confirmation/", json={"user_id": 1})

print(response.text)

print(requests.post("http://192.168.0.48:4121/mail-confirmation/api/v1.0/check-confirmation/", json={"key": response.json()["key"]}).text)