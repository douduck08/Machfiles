## Git Cheatsheet

### Setting alias
```
git config --global alias.st status
git config --global alias.ci commit
git config --global alias.co checkout
git config --global alias.br branch
```

### Git commands
```bash
# Branch
git br -m <newname> # rename
git br -d <name> # delete

# Revert
git reset --soft HEAD^ # cancel commit
git reset --hard HEAD  # revert to last commit
git checkout HEAD -- <path> # revert a certain file/folder
git revert HEAD  # revert and commit the reverting

# Remove untracked files or folders after Pull
git clean -f
git clean -d
git clean -n # list but do nothing

# Reload ignore setting
git rm -rf --cached .
```