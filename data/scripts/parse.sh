#!/bin/bash

bedops --everything ../tidy/*.bed | bedmap --echo --count --bases --delim '\t' - > ../tidy/result.bed

