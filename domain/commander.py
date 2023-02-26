from enum import Enum


class Commander:

    def __init__(self, side_id, id, name, description, health = 0) -> None:
        self.side_id = side_id
        self.id = id
        self.name = name
        self.description = description
        self.health = health
        self.image_folder = "../../../Data/Images/CommanderImages/" + id + ".png" # проверить, написать норм путь

    def __str__(self) -> str:
        return self.name


class CommanderSide(Enum):
    RUSSIAN_EMPIRE = 0
    FRENCH_EMPIRE = 1