import gym
import constants
import state_space
import q_table as q_table_module
import q_learning_algorithms
import heuristic_reward as heuristic_reward_module

env = gym.make('MountainCar-v0') # https://github.com/openai/gym/blob/master/gym/envs/classic_control/mountain_car.py

q_table = q_table_module.create_q_table()

epsilon = constants.EPSILON_GREEDY_FACTOR_MAXIMUM
epsilon_decay = (constants.EPSILON_GREEDY_FACTOR_MAXIMUM - constants.EPSILON_GREEDY_FACTOR_MINIMUM) / constants.EPISODES_COUNT

average_reward_counter = 0

for i_episode in range(constants.EPISODES_COUNT):
    previous_observation = env.reset()
    previous_state = state_space.get_state(previous_observation, env)
    previous_action = 1
    done = False
    total_reward = 0

    while not done:
        if i_episode > constants.EPISODES_COUNT - constants.EVALUATION_EPISODES_COUNT:
            env.render()

        current_action = q_learning_algorithms.get_next_action(q_table, previous_state, epsilon)
        observation, reward, done, info = env.step(current_action)

        current_state = state_space.get_state(observation, env)

        heuristic_reward = heuristic_reward_module.get_reward(observation, previous_observation, reward)

        q_learning_algorithms.update_q_value(q_table, previous_state, previous_action, heuristic_reward, current_state)

        previous_observation = observation
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
