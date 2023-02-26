from operator import truediv
from domain.card import Card
from typing import List

class Effects:

    dice_bonus = 0
    attack = 0
    attack_knockout = 0
    attack_commander = 0
    attack_both = False
    defence_commander = 0
    defence_knockout = 0
    defence_battalion = 0
    heal_commander = 0
    heal_camp = 0
    keep_operation = False
    unique_battalion_ability = False

    def __init__(self, cannon: Card = None, battalion: Card = None, operation: Card = None, dice: int = -100, commander_health: int = 0):
        if cannon != None:
            self._check_battalion(battalion)
            self._check_cannon(cannon)
            self._check_both(cannon, battalion)
            self._check_dice_effects(battalion, operation, dice, commander_health)
    
    def _check_battalion(self, battalion: Card):
        if battalion.id == "battalionBlue":
            self.attack_knockout += 2
        if battalion.id == "battalionPurple":
            self.attack_commander += 2
        if battalion.id == "battalionTactical":
            self.keep_operation = True
        if battalion.id == "battalionUnique":
            self.attack += 2
        if battalion.id == "battalionWhite":
            self.dice_bonus += 1

    def _check_cannon(self, cannon: Card):
        if cannon.id == "cannonBlue":
            self.defence_knockout += 2
        if cannon.id == "cannonGreen":
            self.defence_commander += 2
        if cannon.id == "cannonSilver":
            self.attack_both = True
        if cannon.id == "cannonWhite":
            self.dice_bonus += 1

    def _check_both(self, cannon: Card, battalion: Card):
        if battalion.id == "battalionWhite" and cannon.id == "cannonWhite":
            self.dice_bonus += 1
        elif battalion.id == "battalionGreen" and cannon.id == "cannonGreen":
            self.attack += 3
        elif battalion.id == "battalionRed" and cannon.id == "cannonRed":
            self.attack += 2
        elif battalion.id == "battalionBlue" and cannon.id == "cannonBlue":
            self.attack_knockout += 4
        elif battalion.id == "battalionPurple" and cannon.id == "cannonPurple":
            self.defence_battalion += 2

    def _check_dice_effects(self, battalion: Card, operation: Card, dice: int, commander_health: int):
        if battalion.id == "battalionGold" and self._dice_check(dice, [5, 6]):
            self.heal_camp += 3
        if battalion.id == "battalionRed" and self._dice_check(dice, [1, 2]):
            self.attack += 4
        if battalion.id == "cannonLegendary":
            self.attack += commander_health
        if battalion.id == "battalionUnique" and self._dice_check(dice, [4, 5, 6]):
            self.unique_battalion_ability = True
        if battalion.id == "cannonFamily":
            self.heal_commander += dice

    def _dice_check(self, dice: int, values: List[int]):
        for v in values:
            if dice - self.dice_bonus <= v <= dice + self.dice_bonus:
                return True
        return False
