
from typing import List
from domain.commander import Commander, CommanderSide
import random

class HeadQuarter:
    
    def __init__(self, commanders):
        self.commanders = commanders.copy()
        for key in commanders.keys():
            random.shuffle(self.commanders[key])

    def pick_ru(self):
        return self._pick(CommanderSide.RUSSIAN_EMPIRE.value)

    def pick_fr(self):
        return self._pick(CommanderSide.FRENCH_EMPIRE.value)

    def swap(self, to_swap: Commander) -> Commander:
        return self._pick(to_swap.side_id, to_swap)

    def _pick(self, side_id, to_swap: Commander = None) -> Commander:
        to_return = self.commanders[side_id].pop(0)
        if to_swap != None:
            self.commanders[side_id].append(to_swap)
        
        random.shuffle(self.commanders[side_id])
        return to_return

    def __str__(self) -> str:
        return str(self.commanders[CommanderSide.RUSSIAN_EMPIRE.value]) + ' ru, ' + str(self.commanders[CommanderSide.FRENCH_EMPIRE.value]) + ' fr'
