from django.db import models

class User(models.Model):
    tgid = models.CharField(primary_key=True, max_length=512)
    name = models.CharField(max_length=512)
    referal_id = models.CharField(max_length=512)
    balance = models.IntegerField(default=0)

class TradeCourse(models.Model):
    id = models.IntegerField(primary_key=True)
    date = models.DateTimeField()
    course = models.FloatField()