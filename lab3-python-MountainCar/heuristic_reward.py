def get_reward(observation, reward):
    car_position_raw, car_velosity_raw = observation
    if (reward == 0): # won the game
        return 10000

    close_to_finish_reward = 0
    if (car_position_raw > 0 and car_velosity_raw > 0):
        close_to_finish_reward = 3

    return round(abs(car_velosity_raw)*100) + close_to_finish_reward