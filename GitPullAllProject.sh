# This script will run 'git pull' in all sub-dir.
for dir in */ ; do
  echo "===== $dir ===== "
  (cd $dir && git pull)
done