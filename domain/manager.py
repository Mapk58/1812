from domain.head_quarter import HeadQuarter
from domain.commander import Commander, CommanderSide
from domain.deck import Deck
from domain.card import Card
from domain.game import Game
from domain.tablet import Tablet
from domain.effects import Effects
from domain.player import Player
import json


class Manager:

    ROOT_PATH = './Data/Cards'

    def json_loader(self, path):
        with open(path, 'r', encoding='utf-8') as file:
            print(path)
            result = json.load(file)
        return result

    def create_hq(self, deck_type: str = 'classicHQ') -> HeadQuarter:
        path =  self.ROOT_PATH + '/HQData/' + deck_type + '.json'
        names_path = self.ROOT_PATH + '/HQData/commanderNames.json'
        names = self.json_loader(names_path)['commanderNames']

        hq = self.json_loader(path)
        commanders = {
            CommanderSide.FRENCH_EMPIRE.value: [],
            CommanderSide.RUSSIAN_EMPIRE.value: []
        }

        for commander_name in names:
            for _ in range(hq[commander_name]):
                to_add = self.create_commander(commander_name)
                commanders[to_add.side_id].append(to_add)
        
        to_return = HeadQuarter(commanders)
        # TODO: logger
        print('HQ ' + str(to_return) + 'was assembled')
        return to_return

    def create_commander(self, commander_name) -> Commander:
        path = self.ROOT_PATH + '/CommandersData/' + commander_name + '.json'
        object = self.json_loader(path)
        to_return = Commander(object['Side'], object['ID'], object['Name'], object['Description'], object['Health'])
        # TODO: logger
        print('Commander ' + str(to_return) + ' has arrived')
        return to_return

    def create_deck(self, deck_type='classicDeck'):
        path = self.ROOT_PATH + '/DeckData/' + deck_type + '.json'
        namesPath = self.ROOT_PATH + '/DeckData/cardNames.json'
        cardNames = self.json_loader(namesPath)['cardNames']
        type_counter = self.json_loader(path) 
        deck = [self.create_card(card) for card in cardNames for _ in range(type_counter[card])]
        to_return = Deck(deck, [])
         # TODO: logger
        print('Deck ' + str(to_return) + ' was created')
        return to_return

    def create_card(self, card_name: str):
        path = self.ROOT_PATH + '/CardsData/' + card_name + '.json'
        object = self.json_loader(path)
        type_id = object['Type']
        to_return = Card(type_id, object['ID'], object['Name'], object['Description'], object['KnockStat'], object['ReplacementStat'], object['MainStat'])
        # TODO: logger
        print('Card ' + str(to_return) + ' was created')
        return to_return
    
    def create_tablet(self, commander: Commander):
        return Tablet(10, 26, None, None, None, commander, Effects())

    def create_game(self):
        deck = self.create_deck()
        hq = self.create_hq()

        hand_one = deck.get_cards(6)
        hand_two = deck.get_cards(6)

        player1 = Player(self.create_tablet(hq.pick_fr), hand_one)
        player2 = Player(self.create_tablet(hq.pick_ru), hand_two)
    
        return Game(player1, player2, deck, hq, 0)
