import tabula
import os

filePath = r"C:/Users/howel/Downloads/Carnegie 2013.pdf"
returnFilePath = r"C:/Users/howel/Downloads/Carnegie 2013.csv"

# this reads page 1
# dfs = tabula.read_pdf(filePath, guess=False, pages=1, stream=True, encoding="utf-8",
#                       area=(96, 24, 558, 750), columns=(24, 127, 220, 274, 298, 325, 343, 364, 459, 545, 591, 748))
# dfs is a list of data frames, you can access the first one with dfs[0]


dfs = tabula.readpdf(filePath)


dfs.to_csv(returnFilePath, index=False)