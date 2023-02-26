from enum import Enum
from typing import List
from domain.card import Card, CardType

class ActionType(Enum):
    AttackCommander = 0
    DamageCamp = 1
    UseOperation = 2
    PerformKnockout = 3
    

def _get_input_in_rage(max_val):
    num = int(input())

    while not 0 <= num < max_val:
        print('Пожалуйста, ведите правильный индекс опции!')
        num = int(input())
    
    return num

class ConcoleApiProvider:

    def __init__(self):
        print('Hello from console api provider')

    def choose_action(actions: List[ActionType] = [ActionType.AttackCommander, ActionType.DamageCamp, ActionType.UseOperation]):
        print('\nВыберите тип действия, которое будете совершать: ')
        
        for i, action in enumerate(actions):
            print(i, action)

        return actions[_get_input_in_rage(len(actions))] # api
        
    def choose_changes(hand: List[Card]):
        print('Выберите карты на замену: ')

        return [hand[_get_input_in_rage(len(hand))]] # api

    def choose_cards_to_keep(self, hand: List[Card]):
        print('Выберите 6 карт, которые хотите оставить: ', hand, 'индексы должны быть разделены пробелом')
        ids = [int(x) for x in input().split()]
        
        return [hand[i] for i in ids] # api

    def choose_your(hand: List[Card]):
        print('Выберите карту, которую хотите заменить: ')
        for i, card in enumerate(hand):
            print(i, card)

        return hand[_get_input_in_rage(len(hand))] # api

    def choose_teir(hand: List[Card]):
        print('Выберите карту, которую хотите забрать: ')
        for i, card in enumerate(hand):
            print(i, card)

        return hand[_get_input_in_rage(len(hand))] # api   

    def choose_what_to_knockput_with(hand: List[Card]):
        print('Выберите, чем хотите выбивать: ')

        cannons = [card for card in hand if card.type == CardType.Cannon]

        for i, card in enumerate(cannons):
            print(i, card)

        return cannons[_get_input_in_rage(len(cannons))] # api

    def choose_cards_to_place_on_tablet(hand: List[Card]):
        print('Ваши карты:')
        for i, card in enumerate(hand):
            print(str(i) + '.', card.describe(), '\n')
        print('\nВыберите карты, которые хотите разместить на поле: ')
        idx = [int(x) for x in input('Индекс через пробел: ').split()]
        return [hand[i] for i in idx] # api


class Api(ConcoleApiProvider):

    def __init__(self):
        ConcoleApiProvider.__init__(self)
    #     pass

    # def choose_action(actions: List[ActionType]):
    #     print('Выберите тип действия, которое будете совершать: ')
        
    #     for i, action in enumerate(actions):
    #         print(i, action)

    #     decisiom = ActionType.DamageCamp # api
        
    # def choose_changes(hand: List[Card]):
    #     print('Выберите карты на замену: ')

    #     return [hand[0]] # api

    # def choose_cards_to_keep(hand: List[Card]):
    #     print('Выберите 6 карт, которые хотите оставить: ')

    #     return hand[:6] # api

    # def choose_your(hand: List[Card]):
    #     print('Выберите карту, которую хотите заменить: ')
    #     for i, card in enumerate(hand):
    #         print(i, card)

    #     return hand[0] # api

    # def choose_teir(hand: List[Card]):
    #     print('Выберите карту, которую хотите забрать: ')
    #     for i, card in enumerate(hand):
    #         print(i, card)

    #     return hand[0] # api   

    # def choose_what_to_knockput_with(hand: List[Card]):
    #     print('Выберите, чем хотите выбивать: ')

    #     cannons = [card for card in hand if card.type == CardType.Cannon]

    #     for i, card in enumerate(cannons):
    #         print(i, card)

    #     return [cannons[0]] # api

    # def choose_cards_to_place_on_tablet(hand: List[Card]):
    #     print('Выберите карты, которые хотите разместить на поле: ')
    #     return hand[0] # api
