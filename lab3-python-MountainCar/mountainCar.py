import gym
import math
import random

env = gym.make('MountainCar-v0') # https://github.com/openai/gym/blob/master/gym/envs/classic_control/mountain_car.py
EPISODES_COUNT = 5000
LEARNING_FACTOR = 0.2 # learning factor
DISCOUNT_FACTOR = 0.9 # discount factor
EPSILON_GREEDY_FACTOR_MINIMUM = 0
EPSILON_GREEDY_FACTOR_MAXIMUM = 0.8
RANDOM_Q_VALUE_MINIMUM = -1
RANDOM_Q_VALUE_MAXIMUM = 1
ROUND_DIGITS = 3
STATE_POSITION_BUCKETS = 150
STATE_VELOCITY_BUCKETS = 120

actions = [
    0, # move left
    1, # stay
    2, # move right
]

def generate_random_q_values():
    q_values = []
    for _ in range(len(actions)):
        q_values.append(random.randint(RANDOM_Q_VALUE_MINIMUM, RANDOM_Q_VALUE_MAXIMUM))

    return q_values

def get_q_value(q_table, state, action):
    q_values = q_table[state]
    return q_values[action]

def set_q_value(q_table, state, action, value):
    q_values = q_table[state]
    q_values[action] = value

def get_max_q_value(q_table, state):
    max_q_value = -math.inf
    for action in actions:
        current_q_value = get_q_value(q_table, state, action) 
        if (current_q_value > max_q_value):
            max_q_value = current_q_value
    return max_q_value

def get_next_action(q_table, state, epsilon):
    if random.random() < epsilon:
        return get_random_action()
    return get_optimal_action(q_table, state)

def get_optimal_action(q_table, state):
    optimal_action = -1
    optimal_action_q_value = -math.inf
    for action in actions:
        q_value = get_q_value(q_table, state, action)
        if (q_value > optimal_action_q_value):
            optimal_action = action
            optimal_action_q_value = q_value
    return optimal_action

def get_random_action():
    return random.randint(0, len(actions) - 1)

def get_state(observation):
    car_position_raw, car_velosity_raw = observation
    return get_state_from_velocity_and_position(normalize_car_position(car_position_raw), normalize_car_velocity(car_velosity_raw))

def normalize_car_position(position_raw):
    position_min = env.observation_space.low[0]
    position_max = env.observation_space.high[0]

    step = (position_max - position_min) / STATE_POSITION_BUCKETS

    return round((position_raw - position_min) / step)

def normalize_car_velocity(velocity_raw):
    velocity_min = env.observation_space.low[1]
    velocity_max = env.observation_space.high[1]

    step = (velocity_max - velocity_min) / STATE_VELOCITY_BUCKETS

    return round((velocity_raw - velocity_min) / step)

def update_q_value(q_table, state, action, reward, next_state):
    current_value = get_q_value(q_table, state, action)
    new_value = current_value +\
                                LEARNING_FACTOR * (
                                    reward
                                    + DISCOUNT_FACTOR * get_max_q_value(q_table, next_state)
                                    - current_value)
    set_q_value(q_table, state, action, new_value)

def create_q_table():
    new_q_table = {}
    
    for velocity in range(STATE_VELOCITY_BUCKETS):
        for position in range(STATE_POSITION_BUCKETS):
            state = get_state_from_velocity_and_position(velocity, position)
            new_q_table[state] = generate_random_q_values()

    return new_q_table

def get_state_from_velocity_and_position(velocity, position):
    return velocity * STATE_POSITION_BUCKETS + position

q_table = create_q_table()

epsilon = EPSILON_GREEDY_FACTOR_MAXIMUM
epsilon_decay = (EPSILON_GREEDY_FACTOR_MAXIMUM - EPSILON_GREEDY_FACTOR_MINIMUM) / EPISODES_COUNT

for i_episode in range(EPISODES_COUNT):
    observation = env.reset()
    previous_state = get_state(observation)
    previous_action = 1
    done = False
    total_reward = 0

    while not done:
        if i_episode > EPISODES_COUNT - 50:
            env.render()

        current_action = get_next_action(q_table, previous_state, epsilon)
        observation, reward, done, info = env.step(current_action)

        current_state = get_state(observation)

        if done and reward != -1:
            set_q_value(q_table, previous_state, previous_action, reward)
        else:
            update_q_value(q_table, previous_state, previous_action, reward, current_state)
        previous_action = current_action
        previous_state = current_state
        total_reward += reward 
    
    if (i_episode % 100 == 0):
        print(f'Episode number: {i_episode}, TotalReward: {total_reward}')

    epsilon -= epsilon_decay

env.close()
