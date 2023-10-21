import sqlalchemy

from .db_session import SqlAlchemyBase


class MainConfirmation(SqlAlchemyBase):
    __tablename__ = 'urls'

    id = sqlalchemy.Column(
        sqlalchemy.Integer,
        primary_key=True,
        unique=True,
        nullable=False,
        autoincrement=True
    )

    user_id = sqlalchemy.Column(
        sqlalchemy.Integer,
        nullable=False
    )

    url = sqlalchemy.Column(
        sqlalchemy.String,
        nullable=False,
    )

    datetime = sqlalchemy.Column(
        sqlalchemy.String,
        nullable=False
    )

    def __str__(self):
        return "MainConfirmation(id={0}, user_id={1}, url={2}, datetime={3})".format(
            self.id, self.user_id, self.url, self.datetime
        )
