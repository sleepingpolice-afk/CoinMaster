package wesleyfrederickoh.wesleyfrederickohUnity.service;

import wesleyfrederickoh.wesleyfrederickohUnity.dto.PlayerPublicDTO;
import wesleyfrederickoh.wesleyfrederickohUnity.model.Player;
import wesleyfrederickoh.wesleyfrederickohUnity.repository.PlayerRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.UUID;
import java.util.stream.Collectors;

@Service
public class PlayerService {
    @Autowired
    private PlayerRepository playerRepository;

    public Player createPlayer(Player player) {
        return playerRepository.save(player);
    }

    public Optional<Player> getPlayerByUsername(String username) {
        return playerRepository.findByUsername(username);
    }

    public Optional<Player> getPlayerByEmailAndPassword(String email, String password) {
        return playerRepository.findByEmailAndPassword(email, password);
    }

    public Optional<Long> getCurrencyByUsername(String username) {
        return playerRepository.findByUsername(username).map(Player::getCurrency);
    }

    public List<PlayerPublicDTO> getRandomPlayers(UUID excludePlayerId) {
        List<Player> randomPlayers = playerRepository.findRandomPlayers(excludePlayerId);

        return randomPlayers.stream()
                .map(player -> new PlayerPublicDTO(
                        player.getPlayerId(),
                        player.getUsername(),
                        player.getCurrency()))
                .collect(Collectors.toList());
    }
}
