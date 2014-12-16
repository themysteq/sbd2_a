#!/usr/bin/env python
# -*- coding: utf-8 -*-

#from abc import ABCMeta, abstractmethod

class Record(object):
	def get_meta(self, record):
		sum_of_bits = 0
		for i in record:
			sum_of_bits += bin(i).count('1')
		return sum_of_bits

	def __init__(self,record_body = None, pointer = 0):
		self.record_body = bytearray(record_body)
		self.pointer = pointer
		self.meta = self.get_meta(self.record_body)

class Page(object):
	def __init__(self, records = None, offset = 0, key_value = -1):
		self.records = records
		#self.top_key = -1
"""

class FileManager(object):
	def __init__(self,_db_file = None):
		#plik, który trzyma sie pod managerem
		self.db_file = _db_file

	def getPage(self):
"""		
		
