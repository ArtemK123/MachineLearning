import gym
import math
import random

env = gym.make('MountainCar-v0') # https://github.com/openai/gym/blob/master/gym/envs/classic_control/mountain_car.py
EPISODES_COUNT = 100
MAX_STEPS_COUNT = 200  # maximum in 200 steps is built into the env
LEARNING_FACTOR = 1 # learning factor
DISCOUNT_FACTOR = 1 # discount factor
EPSILON_GREEDY_FACTOR = 0 # epsilon-greedy factor 
RANDOM_Q_VALUE_MINIMUM = -10
RANDOM_Q_VALUE_MAXIMUM = 10

actions = [
    0, # move left
    1, # stay
    2, # move right
]

Q_table = {} # state_key: [action1_q_value, action2_q_value, action3_q_value]

def generate_random_q_values():
    q_values = []
    for _ in range(len(actions)):
        q_values.append(random.randint(RANDOM_Q_VALUE_MINIMUM, RANDOM_Q_VALUE_MAXIMUM))

    return q_values

def get_q_value(state, action):
    if (state not in Q_table):
        Q_table[state] = generate_random_q_values()
    
    q_values = Q_table[state]
    return q_values[action]

def set_q_value(state, action, value):
    if (state not in Q_table):
        Q_table[state] = generate_random_q_values()

    q_values = Q_table[state]
    q_values[action] = value

def get_max_q_value(state):
    max_q_value = -math.inf
    for action in actions:
        current_q_value = get_q_value(state, action) 
        if (current_q_value > max_q_value):
            max_q_value = current_q_value
    return max_q_value

def get_next_action(state):
    if random.random() < EPSILON_GREEDY_FACTOR:
        return get_random_action()
    return get_optimal_action(state)

def get_optimal_action(state):
    optimal_action = -1
    optimal_action_q_value = -math.inf
    for action in actions:
        q_value = get_q_value(state, action)
        if (q_value > optimal_action_q_value):
            optimal_action = action
            optimal_action_q_value = q_value
    return optimal_action

def get_random_action():
    return random.randint(0, len(actions) - 1)

def get_state(observation):
    car_position, car_velosity = observation
    round_digits = 3
    return f'{round(car_position, round_digits)}, {round(car_velosity, round_digits)}'

def update_q_value(state, action, reward, next_state):
    current_value = get_q_value(state, action)
    new_value = current_value + LEARNING_FACTOR * (reward + DISCOUNT_FACTOR * get_max_q_value(next_state) - current_value)
    set_q_value(state, action, new_value)

def run_episode():
    observation = env.reset()
    previous_state = get_state(observation)
    previous_action = 1
    for _ in range(MAX_STEPS_COUNT):
        env.render()

        current_action = get_next_action(previous_state)
        observation, reward, done, info = env.step(current_action)

        if done:
            break

        current_state = get_state(observation)

        update_q_value(previous_state, previous_action, reward, current_state)
        previous_action = current_action
        previous_state = current_state

for i_episode in range(EPISODES_COUNT):
    print(f'Episode number: {i_episode}')
    run_episode()

env.close()
