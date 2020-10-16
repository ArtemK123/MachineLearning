from sklearn.feature_extraction.text import CountVectorizer
from sklearn.linear_model import LogisticRegression
from sklearn.model_selection import train_test_split

def get_score(text_input, y_input, C, k):
    x_text_train, x_text_test, y_train, y_test = train_test_split(text_input, y_input, test_size=k, random_state=0)

    cv = CountVectorizer()
    cv.fit(x_text_train)
    x_train = cv.transform(x_text_train)
    x_test = cv.transform(x_text_test)

    logit = LogisticRegression(C=C, n_jobs=-1)
    logit.fit(x_train, y_train)

    return logit.score(x_test, y_test)