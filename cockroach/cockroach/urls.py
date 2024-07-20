from django.contrib import admin
from django.urls import path, include
from game import urls as game_urls
from dal import urls as dal_urls

urlpatterns = [
    path('admin', admin.site.urls),
    path('game/', include(game_urls.urlpatterns)),    
    path('dal/', include(dal_urls.urlpatterns)),    
]
