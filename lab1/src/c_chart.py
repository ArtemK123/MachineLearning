import pandas as pd
import matplotlib.pyplot as plt
from get_score import get_score

df = pd.read_csv('../resources/wikipedia-detox-250-line-data.tsv', sep='\t')

text_input = df['SentimentText']
y_input = df['Sentiment']

C_array = [0.25, 0.5, 1, 2, 4, 8, 16, 32]

scores = list(map(lambda C: get_score(text_input, y_input, C, 0.25), C_array))

plt.plot(C_array, scores)

plt.xlabel("C (log scale)")
plt.ylabel("Score")

plt.xscale("log")

plt.show()