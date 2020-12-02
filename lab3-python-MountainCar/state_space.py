import math
import constants

def get_state(observation, env):
    car_position_raw, car_velosity_raw = observation
    return compose_state(normalize_car_position(car_position_raw, env), normalize_car_velocity(car_velosity_raw, env))

def normalize_car_position(position_raw, env):
    position_min = env.observation_space.low[0]
    position_max = env.observation_space.high[0]

    step = (position_max - position_min) / constants.STATE_POSITION_BUCKETS

    return math.floor((position_raw - position_min) / step)

def normalize_car_velocity(velocity_raw, env):
    velocity_min = env.observation_space.low[1]
    velocity_max = env.observation_space.high[1]

    step = (velocity_max - velocity_min) / constants.STATE_VELOCITY_BUCKETS

    return math.floor((velocity_raw - velocity_min) / step)

def compose_state(position, velocity):
    return velocity * constants.STATE_POSITION_BUCKETS + position