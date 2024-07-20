from django.contrib import admin
from .models import User, Task, User_Task

admin.site.register(User)
admin.site.register(Task)
admin.site.register(User_Task)
