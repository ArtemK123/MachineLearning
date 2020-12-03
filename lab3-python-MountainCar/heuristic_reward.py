import constants
import state_space

def get_reward(observation, reward):
    if (reward == 0): # won the game
        return 10000

    position_raw, velocity_raw = observation
    velocity_reward = round(abs(velocity_raw)*100)

    if (position_raw < -0.10 or position_raw > 0.2):
        if (velocity_raw > 0):
            return velocity_reward + 5
        else:
            return velocity_reward - 1
    else:
        return velocity_reward