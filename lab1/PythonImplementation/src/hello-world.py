import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
sns.set()

df = pd.read_csv('../resources/wikipedia-detox-250-line-data.tsv', sep='\t')

df['Sentiment'].hist()