import os

from flask import Flask, jsonify, make_response, request

from ..db import *
from ..config import EMAIL, PASSWORD
from .urls_generator import URLGenerator

application = Flask(__name__)

if not os.path.exists("./database"):
    os.mkdir("./database")
global_init("./database/urls.db")
url_generator = URLGenerator(EMAIL, PASSWORD)


@application.route("/mail-confirmation/api/v1.0/send-confirmation/", methods=["POST"])
def __mail_confirmation():
    if not request.json:
        return make_response(jsonify({"error": "no data"}), 404)

    new_url = url_generator.generate_url()

    new_user = MainConfirmation()
    new_user.user_id = request.json["user_id"]
    new_user.url = new_url
    new_user.datetime = url_generator.get_datetime()

    session = db_session.create_session()

    session.add(new_user)
    session.commit()

    session.close()

    return make_response(jsonify({"key": new_url}))


@application.route("/mail-confirmation/api/v1.0/check-confirmation/", methods=["POST"])
def __check_confirmation():
    if not request.json:
        return make_response(jsonify({"error": "no data"}), 404)

    session = db_session.create_session()
    user = session.query(MainConfirmation).where(MainConfirmation.url == request.json["key"])
    # session.close()

    if not user or user is None:
        return make_response(jsonify({"error": "user not found"}), 404)

    user = user.first()

    user_id = user.user_id
    url_datetime = user.datetime

    session.delete(user)
    session.commit()
    session.close()

    return make_response(jsonify({"status": url_generator.get_time_status(url_datetime), "user_id": user_id}), 201)
