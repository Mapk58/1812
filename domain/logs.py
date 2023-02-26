
class Logs:

    def __init__(self):
        self.level = 0
        self.keys = ['DBUG', 'INFO', 'WARN', 'FATA']
        self.counts = {key: 0 for key in self.keys}

    def publicate(message: str):
        print(message)

    def _trigger_log(self, key_idx, message: str):
        key = self.keys[key_idx]
        if self.level >= key_idx + 1:
            self.publicate(key + "[" + self.counts[key] + "] " + message)
        self.counts[key] += 1

    def debug(self, message: str):
        return self._trigger_log(0, message)

    def info(self, message: str):
        return self._trigger_log(1, message)

    def warning(self, message: str):
        return self._trigger_log(2, message)

    def error(self, message: str):
        return self._trigger_log(3, message)
