import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
from get_score import get_score

df = pd.read_csv('../resources/wikipedia-detox-250-line-data.tsv', sep='\t')

text_input = df['SentimentText']
y_input = df['Sentiment']

print(get_score(text_input, y_input, 5, 0.25))
