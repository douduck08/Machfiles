# install rbenv
git clone git://github.com/sstephenson/rbenv.git .rbenv

# add settings into ~/.bashrc
echo '# nvm of Ruby' >> ~/.bashrc
echo 'export PATH="$HOME/.rbenv/bin:$PATH"' >> ~/.bashrc
echo 'eval "$(rbenv init -)"' >> ~/.bashrc
echo 'export RBENV_ROOT=/usr/local/var/rbenv' >> ~/.bashrc
echo 'if which rbenv > /dev/null; then eval "$(rbenv init -)"; fi' >> ~/.bashrc

# list available Ruby version
# rbenv install -l

# install Ruby
rbenv install 2.2.3
rbenv local 2.2.3
rbenv rehash