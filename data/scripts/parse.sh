#!/bin/bash

bedops --everything ../tidy/*.bed | bedmap --echo --count --bases --delim '\t' - > all.bed

