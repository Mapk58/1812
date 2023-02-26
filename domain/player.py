from domain.tablet import Tablet
from typing import List
from domain.card import Card, CardType
from domain.interaction import Interaction
from domain.api import Api, ActionType

class Player:

    def __init__(self, tablet: Tablet, hand: List[Card]):
        self.tablet = tablet
        self.hand = hand
        self.to_drop = []

    def clean_buffer(self):
        to_return = self.to_drop.copy()
        self.to_drop = []
        return to_return

    def fill_tablet(self):
        # select action, cannon, 
        self.hand.filter()

    def first_phase(self, dice: int):
        interaction = Interaction()
        allowed_actions = []
        cannon_count = 0
        for i in self.hand:
            if i.type_id is CardType.Cannon:
                cannon_count += 1
        
        if cannon_count != 0:
            allowed_actions.append(ActionType.PerformKnockout)
        
        if self.tablet.operation != None:
            allowed_actions.append(ActionType.UseOperation)
        
        allowed_actions.append(ActionType.AttackCommander)
        allowed_actions.append(ActionType.DamageCamp)

        action = Api.choose_action(allowed_actions)

        if action is ActionType.AttackCommander:
            interaction.Attack = self.tablet.act_commander_attack(dice)

        if action is ActionType.DamageCamp:
            interaction.Attack = self.tablet.act_camp_attack(dice)

        if action is ActionType.UseOperation:
            print("PLAYER FIRST PHASE OPERATION")

        if action is ActionType.PerformKnockout:
            target = Api.choose_what_to_knockout()
            weapon = Api.choose_cards_to_knockout_with(self.hand)
            knockoutAction = self.tablet.act_knockout(weapon, target)

            for i in weapon:
                self.hand.Remove(i)

            interaction.knock_out = knockoutAction
        return interaction

    