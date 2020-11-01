from sklearn.feature_extraction.text import CountVectorizer
from sklearn.linear_model import LogisticRegression

class SentimentClassificationModel:
    _cv = CountVectorizer()
    _model = None

    def __init__(self, sentiments, results, c):
        self._cv.fit(sentiments)
        x_train = self._cv.transform(sentiments)
        self._model = LogisticRegression(C = c, n_jobs = -1)
        self._model.fit(x_train, results)

    def get_score(self, sentiments, results):
        x_test = self._cv.transform(sentiments)
        return self._model.score(x_test, results)

    def predict(self, sentiment):
        sentiments_array = [sentiment]
        x = self._cv.transform(sentiments_array)[0]
        return self._model.predict(x)
