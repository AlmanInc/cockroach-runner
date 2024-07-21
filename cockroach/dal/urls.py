from django.urls import path
from .views import check_exist, add_user, update_user, add_referal, get_referal, get_cur_course, \
    get_balance, add_balance, get_tasks, commit_task

urlpatterns = [
    path('check_exist', check_exist, name='check_exist'),  
    path('add_user', add_user, name='add_user'),  
    path('update_user', update_user, name='update_user'),  
    path('add_referal', add_referal, name='add_referal'),  
    path('get_referal', get_referal, name='get_referal'),  
    path('get_cur_course', get_cur_course, name='get_cur_course'),  
    path('get_balance', get_balance, name='get_balance'),  
    path('add_balance', add_balance, name='add_balance'),  
    path('get_tasks', get_tasks, name='get_tasks'),  
    path('commit_task', commit_task, name='commit_task'),  
]