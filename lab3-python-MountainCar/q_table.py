import constants
import state_space
import random

def create_q_table():
    new_q_table = {}
    
    for velocity in range(constants.STATE_VELOCITY_BUCKETS):
        for position in range(constants.STATE_POSITION_BUCKETS):
            state = state_space.compose_state(position, velocity)
            new_q_table[state] = generate_random_q_values()

    return new_q_table

def get_q_value(q_table, state, action):
    q_values = q_table[state]
    return q_values[action]

def set_q_value(q_table, state, action, value):
    q_values = q_table[state]
    q_values[action] = value

def generate_random_q_values():
    q_values = []
    for _ in range(len(constants.ACTIONS)):
        q_values.append(random.randint(constants.RANDOM_Q_VALUE_MINIMUM, constants.RANDOM_Q_VALUE_MAXIMUM))

    return q_values