import gym
import math
import random
import constants
import state_space
import q_table as q_table_module

env = gym.make('MountainCar-v0') # https://github.com/openai/gym/blob/master/gym/envs/classic_control/mountain_car.py

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

def get_heuristic_reward(observation, reward):
    car_position_raw, car_velosity_raw = observation
    if (reward == 0): # won the game
        return 10000

    close_to_finish_reward = 0
    if (car_position_raw > 0 and car_velosity_raw > 0):
        close_to_finish_reward = 3

    return round(abs(car_velosity_raw)*100) + close_to_finish_reward

q_table = q_table_module.create_q_table()

epsilon = constants.EPSILON_GREEDY_FACTOR_MAXIMUM
epsilon_decay = (constants.EPSILON_GREEDY_FACTOR_MAXIMUM - constants.EPSILON_GREEDY_FACTOR_MINIMUM) / constants.EPISODES_COUNT

average_reward_counter = 0

for i_episode in range(constants.EPISODES_COUNT):
    observation = env.reset()
    previous_state = state_space.get_state(observation, env)
    previous_action = 1
    done = False
    total_reward = 0

    while not done:
        if i_episode > constants.EPISODES_COUNT - constants.EVALUATION_EPISODES_COUNT:
            env.render()

        current_action = get_next_action(q_table, previous_state, epsilon)
        observation, reward, done, info = env.step(current_action)

        current_state = state_space.get_state(observation, env)

        heuristic_reward = get_heuristic_reward(observation, reward)

        update_q_value(q_table, previous_state, previous_action, heuristic_reward, current_state)

        previous_action = current_action
        previous_state = current_state
        total_reward += reward 

    average_reward_counter += total_reward

    if (i_episode % 100 == 0):
        print(f'Episode number: {i_episode}, AverageReward: {average_reward_counter / 100}, Epsilon: {round(epsilon, 3)}')
        average_reward_counter = 0

    if i_episode > constants.EPISODES_COUNT - constants.EVALUATION_EPISODES_COUNT:
        epsilon = 0
    else:
        epsilon -= epsilon_decay

env.close()
