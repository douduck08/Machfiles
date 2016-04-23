# install nvm
git clone https://github.com/creationix/nvm.git ~/.nvm

# add settings into ~/.bashrc
echo '# nvm of NodeJS' >> ~/.bashrc
echo 'export NVM_DIR=~/.nvm' >> ~/.bashrc
echo '[ -s "$NVM_DIR/nvm.sh" ] && . "$NVM_DIR/nvm.sh"' >> ~/.bashrc
source .bash_profile

# list available node.js version
# nvm ls-remote

# install node.js
nvm install 5.11.0
nvm alias default 5.11.0