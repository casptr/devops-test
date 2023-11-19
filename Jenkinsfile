pipeline {
    agent any

    stages {
        stage('Preparation') {
            steps {
                build job: 'CheckIfDbRunning' 
                build job: 'StartApp'
            }
        }
        stage('Tests') {
            steps {
                echo "Running tests"
            }
        }
    }
}
