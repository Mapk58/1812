from enum import Enum

class Card:

    def __init__(self, type_id, id, name, description, knockStat = 0, replacementStat = 0, mainStat = 0) -> None:
        self.type_id = type_id
        self.type = CardType(type_id)
        self.id = id
        self.name = name
        self.description = description
        self.knock_stat = knockStat
        self.replacement_stat = replacementStat
        self.main_stat = mainStat
        self.image_folder = "../../../Data/Images/CardImages/" + id + ".png" #проверить, написать норм путь

    def __str__(self) -> str:
        return self.id

    def describe(self):
        return 'Тип: {}\tНазвание: {}\nОписание: {}\mKnock: {}\tReplacement: {}\tMain: {}'.format(self.type, self.name, self.description, self.knock_stat, self.replacement_stat, self.main_stat)

class CardType(Enum):
    Battalion = 0
    Cannon = 1
    Operation = 2