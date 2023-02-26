from typing import List
from domain.card import Card
import random

class Deck:

    def __init__(self, cards: List[Card], drop: List[Card]) -> None:
        self.all_cards = cards
        self.deck_cards = cards.copy()
        random.shuffle(self.deck_cards)
        self.throw_cards = drop

    def __str__(self) -> str:
        return 'Deck from ' + str(self.all_cards)

    def put_cards(self, to_put: List[Card]):
        self.throw_cards += to_put

    def _take(self, count: int) -> List[Card]:
        '''
        Removes `count` elements from deck and returns them
        '''
        return [self.deck_cards.pop(0) for _ in range(count)]

    def get_cards(self, count: int):
        if count <= len(self.deck_cards):
            return self._take(count)

        # shuffle to throw and move it to the main deck
        to_return = self._take(len(self.deck_cards))

        self.deck_cards = self.throw_cards
        random.shuffle(self.deck_cards)
        self.throw_cards = []

        return to_return + self._take(count - len(self.to_return))

    @property
    def deck_count(self):
        return len(self.deck_cards)

    @property
    def throw_count(self):
        return len(self.throw_cards)

    @property
    def all_count(self):
        return len(self.all_cards)
