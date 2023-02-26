from domain.player import Player
from domain.deck import Deck
from domain.head_quarter import HeadQuarter
from domain.api import Api
from domain.card import CardType

class Game:

    def __init__(self, player1: Player, player2: Player, deck: Deck, hq: HeadQuarter, turn: int):
        self.player1 = player1
        self.player2 = player2
        self.deck = deck
        self.hq = hq
        self.turn = turn


    def preparings(self):
        to_place1 = Api.choose_cards_to_place_on_tablet(self.player1.hand)
        to_place2 = Api.choose_cards_to_place_on_tablet(self.player2.hand)

        for card in to_place1:
            if card.type == CardType.Battalion:
                self.player1.tablet.set_battalion(card)

            if card.type == CardType.Cannon:
                self.player1.tablet.set_cannon(card)

            if card.type == CardType.Operation:
                self.player1.tablet.set_operation(card)
        
        for card in to_place2:
            if card.type == CardType.Battalion:
                self.player2.tablet.set_battalion(card)

            if card.type == CardType.Cannon:
                self.player2.tablet.set_cannon(card)

            if card.type == CardType.Operation:
                self.player2.tablet.set_operation(card)
            
        self.turn += 1
