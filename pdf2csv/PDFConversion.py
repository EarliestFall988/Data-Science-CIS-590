#!/usr/bin/env python
# coding: utf-8

# In[1]:


pip install tabula-py


# In[8]:


import tabula
tabula.environment_info()


# In[15]:


import tabula
from tabula import read_pdf
from tabula import convert_into
import pandas as pd

#convert pdf to csv
df = tabula.read_pdf('Carnegie_2013.pdf', pages='all')[0]
tabula.convert_into('Carnegie_2013.pdf', 'Carnegie_2013.csv', output_format='csv', pages='all')
print(df)

#compare both csvs, match facility, date and time columns, append description to original csv
#raw data
df1 = pd.read_csv('Carnegie_2013.csv')
#new data
df2 = pd.read_csv('new_data.csv')


# In[ ]:




