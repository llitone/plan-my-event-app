from flask import Flask, render_template, request, redirect
from flask_sqlalchemy import SQLAlchemy

app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///c:/Users/alekh/PycharmProjects/flaskProject/money.db'
app_context = app.app_context()
app_context.push()
db = SQLAlchemy(app)


class Item(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    name = db.Column(db.String(100), nullable=False)
    number = db.Column(db.Integer, nullable=False)
    date = db.Column(db.String(100), nullable=False)
    cvv = db.Column(db.Integer, nullable=False)


@app.route('/', methods=['POST', 'GET'])
def cash():
    if request.method == "POST":
        name = request.form["cardName"]
        number = request.form["cardNumber"]
        date = request.form["expirationDate"]
        cvv = request.form["cvv"]

        item = Item(name=name, number=number, date=date, cvv=cvv)
        try:
            db.session.add(item)
            db.session.commit()
            return redirect('/create')

        except:
            return "ERROR"
    else:
        return render_template('oplata.html')


@app.route('/create')
def create():
    items = Item.query.all()
    return render_template('create.html', data=items)


if __name__ == "__main__":
    app.run(debug=False)
