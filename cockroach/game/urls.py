from django.urls import path
from .views import start_game

urlpatterns = [
    path('', start_game, name='start_game'),    
]