import pandas as pd
from sentiment_classification_model import SentimentClassificationModel

df = pd.read_csv('../resources/wikipedia-detox-250-line-data.tsv', sep='\t')

text_input = df['SentimentText']
y_input = df['Sentiment']

model = SentimentClassificationModel(text_input, y_input, 1)

sentiment = "This is very bad!"
result = model.predict(sentiment)
if (result == 1):
    print("Bad sentiment")
else:
    print("Good sentiment")