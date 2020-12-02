import constants
import math
import random
import q_table as q_table_module

def get_max_q_value(q_table, state): # max
    max_q_value = -math.inf
    for action in constants.ACTIONS:
        current_q_value = q_table_module.get_q_value(q_table, state, action) 
        if (current_q_value > max_q_value):
            max_q_value = current_q_value
    return max_q_value

def get_optimal_action(q_table, state): # argmax
    optimal_action = -1
    optimal_action_q_value = -math.inf
    for action in constants.ACTIONS:
        q_value = q_table_module.get_q_value(q_table, state, action)
        if (q_value > optimal_action_q_value):
            optimal_action = action
            optimal_action_q_value = q_value
    return optimal_action

def get_next_action(q_table, state, epsilon):
    if random.random() < epsilon:
        return get_random_action()
    return get_optimal_action(q_table, state)

def get_random_action():
    return random.randint(0, len(constants.ACTIONS) - 1)

def update_q_value(q_table, state, action, reward, next_state):
    current_value = q_table_module.get_q_value(q_table, state, action)
    new_value = current_value +\
                                constants.LEARNING_FACTOR * (
                                    reward
                                    + constants.DISCOUNT_FACTOR * get_max_q_value(q_table, next_state,)
                                    - current_value)
    q_table_module.set_q_value(q_table, state, action, new_value)