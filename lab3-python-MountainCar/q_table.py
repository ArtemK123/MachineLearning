import constants
import state_space
import random

class QTable:
    def __init__(self, table):
      self.table = table
  
    def get_value(self, state, action):
        q_values = self.table[state]
        return q_values[action]

    def set_value(self, state, action, value):
        q_values = self.table[state]
        q_values[action] = value

def create_q_table():
    new_table = {}
    
    for velocity in range(constants.STATE_VELOCITY_BUCKETS):
        for position in range(constants.STATE_POSITION_BUCKETS):
            state = state_space.compose_state(position, velocity)
            new_table[state] = _generate_random_actions_array()

    return QTable(new_table)

def _generate_random_actions_array():
    q_values = []
    for _ in range(len(constants.ACTIONS)):
        q_values.append(random.randint(constants.RANDOM_Q_VALUE_MINIMUM, constants.RANDOM_Q_VALUE_MAXIMUM))

    return q_values