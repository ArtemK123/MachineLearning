import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
from get_score import get_score

df = pd.read_csv('../resources/wikipedia-detox-250-line-data.tsv', sep='\t')

text_input = df['SentimentText']
y_input = df['Sentiment']

k_array = [0.05, 0.1, 0.15, 0.2, 0.25, 0.3, 0.4, 0.5]

scores = list(map(lambda k: get_score(text_input, y_input, 1, k), k_array))

plt.plot(k_array, scores)

plt.xlabel("k")
plt.ylabel("Score")

plt.show()