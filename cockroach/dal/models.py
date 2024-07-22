from django.db import models
import uuid
import django
import django.utils
import django.utils.timezone

class User(models.Model):
    tgid = models.CharField(primary_key=True, max_length=512, editable=False)
    name = models.CharField(max_length=512)
    referal_id = models.CharField(max_length=512)
    balance = models.IntegerField(default=500)
    last_visit = models.DateTimeField(default=django.utils.timezone.now)

class Task(models.Model):    
    id = models.UUIDField(primary_key=True, default=uuid.uuid4, editable=False)
    name = models.TextField(max_length=50)
    description = models.CharField(max_length=256, default='')
    cost = models.IntegerField()    
    type = models.IntegerField(default=0)
    params = models.CharField(max_length=512, default='')        
    user = models.ManyToManyField(User, through='User_Task')

class User_Task(models.Model):
    user = models.ForeignKey(User, on_delete=models.DO_NOTHING)
    task = models.ForeignKey(Task, on_delete=models.DO_NOTHING)

    done = models.BooleanField(null=False, default=False)            
    done_date = models.DateTimeField(null=True)
    created = models.DateTimeField(auto_now=True)

    
class TradeCourse(models.Model):
    id = models.IntegerField(primary_key=True)
    date = models.DateTimeField()
    course = models.FloatField()