import pandas as pd
from sklearn.model_selection import train_test_split
df = pd.read_csv('../resources/wikipedia-detox-250-line-data.tsv', sep='\t')
text_input = df['SentimentText']
y_input = df['Sentiment']

from sklearn.model_selection import train_test_split
X_text_train, X_text_test, y_train, y_test = train_test_split(text_input, y_input, test_size=0.25, random_state=0)

from sklearn.feature_extraction.text import CountVectorizer
cv = CountVectorizer()
cv.fit(X_text_train)
X_train = cv.transform(X_text_train)
X_test = cv.transform(X_text_test)

from sklearn.linear_model import LogisticRegression
logit = LogisticRegression(C = 5, n_jobs = -1)
logit.fit(X_train, y_train)

print(logit.score(X_test, y_test))