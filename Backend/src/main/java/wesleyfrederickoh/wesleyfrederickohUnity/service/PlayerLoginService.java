package wesleyfrederickoh.wesleyfrederickohUnity.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import wesleyfrederickoh.wesleyfrederickohUnity.dto.PlayerDataResponse;
import wesleyfrederickoh.wesleyfrederickohUnity.dto.PlayerLoginRequest;
import wesleyfrederickoh.wesleyfrederickohUnity.model.Player;
import wesleyfrederickoh.wesleyfrederickohUnity.model.PlayerUpgrade;
import wesleyfrederickoh.wesleyfrederickohUnity.repository.PlayerRepository;
import wesleyfrederickoh.wesleyfrederickohUnity.repository.PlayerUpgradeRepository;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Optional;

@Service
public class PlayerLoginService {

    @Autowired
    private PlayerRepository playerRepository;

    @Autowired
    private PlayerUpgradeRepository playerUpgradeRepository;

    @Transactional(readOnly = true)
    public PlayerDataResponse loginAndGetPlayerData(PlayerLoginRequest loginRequest) {
        Optional<Player> playerOptional = playerRepository.findByEmail(loginRequest.getEmail());

        if (playerOptional.isEmpty()) {
            throw new RuntimeException("Player not found with email: " + loginRequest.getEmail());
        }

        Player player = playerOptional.get();

        if (!player.getPassword().equals(loginRequest.getPassword())) {
            throw new RuntimeException("Invalid password.");
        }

        List<PlayerUpgrade> upgrades = playerUpgradeRepository.findAllByPlayer(player);
        Map<String, Integer> upgradeLevels = new HashMap<>();
        for (PlayerUpgrade pu : upgrades) {
            upgradeLevels.put(pu.getUpgradeType().getName(), pu.getLevel());
        }

        return new PlayerDataResponse(
                player.getId(),
                player.getCurrency(),
                player.getCreatedAt(),
                player.getUsername(),
                player.getEmail(),
                upgradeLevels
        );
    }
}
