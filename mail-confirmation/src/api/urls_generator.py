import random
import smtplib
import string

from datetime import datetime
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText


class URLGenerator(object):
    STRING = string.digits + string.ascii_letters

    def __init__(self, email: str, password: str):
        self.email = email
        # self.password = password
        self.server = smtplib.SMTP('smtp.gmail.com: 587')
        self.server.starttls()
        self.server.login(
            email,
            password
        )

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

    def send_key(self, email, key, link):
        text = link + key
        msg = MIMEMultipart()
        msg['From'] = self.email
        msg['To'] = email
        msg['Subject'] = "Подтверждение email"

        msg.attach(MIMEText(text))

        self.server.sendmail(
            msg['From'],
            msg['To'],
            msg.as_string()
        )
        # self.server.quit()
