# Visualizing Overlapping Data Between Replicate Experiments

We perform experiments in the lab that isolate regions of the genome that are interesting in some way. In particular, one assay (Next Generation Capture-C) reports on evidence of physical interactions between one specific region of the genome with all other possible locations. The output of these experiments is a BED file that contains genomic intervals or coordinates. 

In order to increase our confidence in the output of the experiment and identify real signal from background noise, we run each experiment multiple times generating a BED file for each replicate. We are interested in visualizing the intervals of the genome where there is a high degree of overlap between the identified contacts within these BED files. 

Given a set of BED files, produce and visualize data that characterizes the amount of supporting evidence (number of overlapping intervals) for all locations. Bonus points will be given if the solution is able to accept the amount of overlap as input (e.g. user defines how much overlap they require: more than 1 base of overlap or even allow for negative overlap with a specified distance).

## Prepare the data.

The `prep4parse.sh` script accepts an input file directory path and an output files directory path. The input directory is scanned for `.BED` files. All `.BED` files found striped to include only the first three columns. An additional column is added to contain output filename for later reference. The data is also sorted by chromosone and location. Presorting the data speeds computation and is a prerequisite for using the open source `bedmap` tool to analyze the files of basepair overlap.

## Parse the data.

The `parse.sh` script accepts a numeric value indicating the number of overlapping basepairs required to inidicate a match. The value may be negative, meaning that the requested number of matches are found within a proximate range. A directory can also be scecified. The specified directory will be scanned for `*.in.bed` files. These files are the output of the `prep4parse.sh` script above. A `results.out` file is generated into the specified output directory.

## Performance testing

To test the scalability. we started with 25 copies of an [HG38 BED file](https://useast.ensembl.org/info/data/ftp/index.html) with 432604 lines of data in each file. Testing was performed on a Linux laptop with 8 2.7GHz cores Linux and 64GB of RAM and SSD.

Processing the data took less than 1 minute.

Process 50 replicas of the same data took less than 2-1/2 min resulting in an output file with over 21M  21,630,200 lines of data.

| files | # comparisons | processing time
|:-----:|:-------------:|:--------------:
| 10    | 43260400      | 19s
| 25    | 10815100      | 58s
| 50    | 21630200      | 2m 25s

The performance tests can be repeated on the target machine by executing
```
./pertest <num copies>
```
