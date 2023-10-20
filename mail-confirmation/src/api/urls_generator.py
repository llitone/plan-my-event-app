import random
import string

from datetime import datetime


class URLGenerator(object):
    STRING = string.digits + string.ascii_letters

    def __init__(self, login: str, password: str):
        self.login = login
        self.password = password

    def generate_url(self):
        url = ""

        for i in range(4):
            for _ in range(8):
                url += self.STRING[random.randint(0, len(self.STRING) - 1)]
            url += "-"
        return url[:-1]

    @staticmethod
    def get_datetime():
        return datetime.now().strftime("%d.%m.%Y %H:%M:%S")

    @staticmethod
    def get_time_status(create_time):
        now = datetime.now()

        create_time = datetime.strptime(create_time, "%d.%m.%Y %H:%M:%S")
        create_time = (now - create_time)
        return int(create_time.seconds // 60) < 5
