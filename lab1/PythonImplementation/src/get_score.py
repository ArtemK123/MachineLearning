from sklearn.model_selection import train_test_split
from sentiment_classification_model import SentimentClassificationModel

def get_score(text_input, y_input, c, k):
    x_text_train, x_text_test, y_train, y_test = train_test_split(text_input, y_input, test_size=k, random_state=0)

    model = SentimentClassificationModel(x_text_train, y_train, c)

    return model.get_score(x_text_test, y_test)