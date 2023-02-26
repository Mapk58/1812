from domain.tablet import Attack
from domain.card import CardType, Card

class Interaction:

    def __init__(self, attack: Attack = Attack(), knockOut: list[int, CardType]=None, sabotage: bool=False, substitution: bool=False, sub_to_place: Card=None, to_get: CardType=0):
        self.Attack = attack
        self.KnockOut = knockOut
        self.Sabotage = sabotage
        self.Substitution = substitution
        self.sub_to_place = sub_to_place
        self.to_get = to_get
