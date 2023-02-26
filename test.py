from domain.manager import Manager
import random

game = Manager().create_game()
print('Игра создана!')
game.preparings()
print('Игрок 1. Ваши карты:')
for i, card in enumerate(game.player1.hand):
    print(i, card.describe())

dice = random.randint(0, 6)
game.player1.first_phase(dice)


print('Игрок 2. Ваши карты:')
for i, card in enumerate(game.player2.hand):
    print(i, card.describe())

dice = random.randint(0, 6)
game.player2.first_phase(dice)