from typing import List
from domain.card import Card, CardType
from domain.commander import Commander
from domain.effects import Effects

class Attack:
    def __init__(self, commander_attack=0, camp_attack=0) -> None:
        self.commander_attack = commander_attack
        self.camp_attack = camp_attack

class Tablet:

    CAMP_HP_MAX = 26
    COMMANDER_HP_MAX = 10
    to_throw = []
    def __init__(self, _commander_hp, _camp_hp, battalion:Card, cannon:Card, operation:Card, commander:Commander, effects:Effects):
        self._commander_hp = _commander_hp
        self._camp_hp = _camp_hp
        self.battalion = battalion
        self.cannon = cannon
        self.commander = commander
        self.operation = operation
        self.effects = effects
    
    @property
    def commander_hp(self):
        return self._commander_hp
    
    @commander_hp.setter
    def commander_hp(self, value):
        if (value > Tablet.COMMANDER_HP_MAX): 
            self._commander_hp = Tablet.COMMANDER_HP_MAX 
        else: 
            if (value < 0):
                 self._commander_hp = 0 
            else: 
                self._commander_hp = value  
  
    @property
    def camp_hp(self):
        return self._commander_hp
    
    @camp_hp.setter
    def camp_hp(self, value):
        if (value > Tablet.CAMP_HP_MAX): 
            self._camp_hp = Tablet.CAMP_HP_MAX 
        else: 
            if (value < 0):
                 self._camp_hp = 0 
            else: 
                self._camp_hp = value
    
    def set_battalion(self, to_set: Card):
        if self.battalion is None:
                self.battalion = to_set
                return True
        if self.camp_hp - self.battalion.replacement_stat > 0:
            self.camp_hp -= self.battalion.replacement_stat
            Tablet.to_throw.append(self.battalion)
            self.battalion = to_set
            return True
        else:
            return False
    
    def set_cannon(self, to_set: Card):
        if self.cannon is None:
            self.cannon = to_set
            return True
        if self.camp_hp - self.cannon.replacement_stat > 0:
            self.camp_hp -= self.cannon.replacement_stat
            Tablet.to_throw.append(self.cannon)
            self.cannon = to_set
            return True
        else:
            return False
    
    def set_operation(self, to_set:Card):
        if not self.operation is None:
            Tablet.to_throw.append(self.operation)
        self.operation = to_set
        return True
    
    def _check_effects(self, dice=-100):
        self.effects = Effects(self.cannon, self.battalion, None, dice, self.commander_hp)
    
    def act_camp_attack(self, dice):
        self._check_effects(dice)
        attack = Attack()
        if self.effects.attack_both:
            attack.commander_attack += dice
            attack.commander_attack += self.cannon.main_stat
            attack.commander_attack += self.effects.attack
            attack.commander_attack += self.effects.attack_commander
        attack.camp_attack += dice
        attack.camp_attack += self.cannon.main_stat
        attack.camp_attack += self.effects.attack
        return attack
        
    def act_commander_attack(self, dice):
        self._check_effects(dice)
        attack = Attack()
        if self.effects.attack_both:
            attack.camp_attack += dice
            attack.camp_attack += self.cannon.main_stat
            attack.camp_attack += self.effects.attack
        attack.commander_attack += dice
        attack.commander_attack += self.cannon.main_stat
        attack.commander_attack += self.effects.attack
        attack.commander_attack += self.effects.attack_commander
        return attack
    
    def act_knockout(self, from_hand:List[Card], target:List[Card]):
        force = 0
        for i in from_hand:
            if i.type.type_id == CardType.Cannon:
                force += i.main_stat
        self._check_effects()
        Tablet.to_throw += from_hand
        return (self.effects.attack_knockout + force, target)
    
    def def_knockout(self, attack):
        self._check_effects()
        force = attack[0]
        if len(attack[1]) == 1:
            if attack[1][0].type_id == CardType.Battalion: 
                if self.battalion.knock_stat + self.effects.defence_knockout <= force:
                    Tablet.to_throw.append(self.battalion)
                    self.battalion = None
                    return True
            if attack[1][0].type_id == CardType.Cannon:
                if self.cannon.knock_stat + self.effects.defence_knockout <= force:
                    Tablet.to_throw.append(self.cannon)
                    self.cannon = None
                    return True
        else:
            if len(attack[1]) == 2:
                if self.battalion.knock_stat + self.cannon.knock_stat + self.effects.defence_knockout <= force:
                    Tablet.to_throw.append(self.battalion)
                    Tablet.to_throw.append(self.cannon)
                    self.battalion = None
                    self.cannon = None
                    return True
        return False
    
    def defence(self, attack:Attack):
        self._check_effects()
        commander = attack.commander_attack
        camp = attack.camp_attack

        commander -= self.battalion.main_stat
        camp -= self.battalion.main_stat
        commander -= self.effects.defence_battalion
        camp -= self.effects.defence_battalion

        commander -= self.effects.defence_commander

        if commander > 0:
            self.commander_hp -= commander
        if camp > 0:
            self.camp_hp -= camp

        if self.camp_hp > 0:
            return False        
        else:
            return True

    def __str__(self):
        #     self._commander_hp = _commander_hp
        # self._camp_hp = _camp_hp
        # self.battalion = battalion
        # self.cannon = cannon
        # self.commander = commander
        # self.operation = operation
        # self.effects = effects
        return 'Tablet: ' + str(self._commander_hp) + ' ' + str(self._camp_hp) + ' ' +  str(self.battalion) + ' ' +  str(self.cannon) + ' ' +  str(self.operation) + ' ' +  str(self.effects)